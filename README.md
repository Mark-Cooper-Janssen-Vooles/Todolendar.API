# Todolendar.API

This repository is the backend for Todolendar, a project which is a todo list and calendar fusion.

The frontend for Todolendar is found at [Todolendar.UI](https://github.com/Mark-Cooper-Janssen-Vooles/Todolendar.UI).

Features include account creation, sign-in functionality, editing your profile, creating todo's and adding them into a calendar as a scheduled todo. Users will also be able to set SMS notifications for reminding them to plan their week / month / day, as well as for reminding them when they have a scheduled todo coming up.

---

## Planning
Figma designs are available [here](https://www.figma.com/file/ona2QoEu6QzTcyffAervOy/Todolender?node-id=0%3A1&t=KPdD8o2qc6cbYQnZ-0).

A basic database schema is available [here](https://app.diagrams.net/#G1NYqMTprbHGnyYW-6s-Pc1sLVT3hZQu_x).

---

## Technology 

This project is created as a .net web api, using entity framework, swagger, fluent validations, automapper, as well as Json Web Token.

---

## Architectural Patterns 

This project uses dependency injection and the repository pattern. It is an attempt at [onion architecture](https://www.codeguru.com/csharp/understanding-onion-architecture/).

The domain layer can be found in the Models folder, while the repository layer can be found in the repositories folder and the services layer can be found in the controllers folder. 

This project was inspired by a tutorial which can be found [here](https://github.com/Mark-Cooper-Janssen-Vooles/dotnet-web-api).

---

## Running Locally 

First you will need to create a database in MSSQLSERVER named "Todolendar", and set the connection string in appsettings.json (there are examples existing). You will then need to run via the package manager console `Add-Migration` as well as `Update-Database` for entity framework to create the database connection.

If testing the endpoint via swagger, you will need to first create a user and then login. The login endpoint will give you a bearer you must authenticate with to use the other endpoints, each endpoint is accessibile only for that user. 

---

## Releasing 

Changes made to this repository and merged into master will trigger a CICD AWS CodePipeline pipeline on `ap-southeast-2`, and be deployed to an ec2 instance in production. 
The pipeline can be found [here](https://ap-southeast-2.console.aws.amazon.com/codesuite/codepipeline/pipelines/todolender-api-pipeline/view?region=ap-southeast-2) once logged in.

### Source
Source is linked to the github repository and picks up any changes to master and kicks off another execution. It requires github authentication when set up.

### CodeBuild 
Build use the buildspec.yml, running the commands and outputting the artifacts into an s3 bucket.
It requires an IAM role attached which has s3 write permissions. 

### CodeDeploy
Deploy uses the appspec.yml file. It requires to be linked to an the EC2 instance with an IAM role allowing s3 permissions and codeDeploy permissions. The EC2 instance must also have the CodeDeploy agent installed, which has been done in my case using a `User Data` script, attached to the EC2 which runs on startup. 

The appspec uses the beforeInstall, ApplicationStart and ApplicationStop hooks which have their own deploy-scripts.

To expose the API in the browser, these things have to be in place: 

1. The EC2 must have `User Data` set up with the CodeDeploy agent installed for the appspec.yml to work. The below script is the User Data.
  - specify the AMI (linux AMI 2)
  - generate key pair
  - VPC as default
  - subnet as the public one
  - security group as launch-wizard-6
  - in advanced details:
    - choose IAM instance profile (mentioned above)
      - Ideally we use least privilege principals here - it needs AmazonS3ReadOnlyAccess and potentially AWSCodeDeployRole. 
    -add the user data below
  - before attempting to deploy to EC2, test you can access the codeDeploy agent within the EC2 terminal: `sudo service codedeploy-agent status`
    - if something has not worked check the logs, may need to create another EC2 as user data is only run when the instance is first started


````ps
#!/bin/bash
yum update -y
yum install -y ruby
yum install -y aws-cli
cd /home/ec2-user
aws s3 cp s3://aws-codedeploy-ap-southeast-2/latest/install . --region ap-southeast-2
chmod +x ./install
./install auto
````
2. Make sure the security group attached to the EC2 enables traffic for HTTP and HTTPS. In the 'inbound rules' select 'Anywhere'. 
3. You need to run a web server, use Apache in the EC2: 
````ps
#Install the Apache web server by running the following command:
sudo apt-get update
sudo apt-get install apache2

# start service on system boot
sudo systemctl start apache2
sudo systemctl enable apache2

# Configure Apache to allow traffic on port 80 by modifying the firewall settings. If you are using the default firewall, you can use the following command to allow incoming traffic on port 80:
sudo ufw allow 80/tcp

# Modify the Apache configuration file to serve your .NET 6 API. Open the configuration file with a text editor:
sudo nano /etc/apache2/sites-available/todolendar.conf

````