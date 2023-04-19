# Todolendar.API

This repository is the backend for Todolendar, a project which is a todo list and calendar fusion.

The frontend for Todolendar is found at [Todolendar.UI](https://github.com/Mark-Cooper-Janssen-Vooles/Todolendar.UI).

Features include account creation, sign-in functionality, editing your profile, creating todo's and adding them into a calendar as a scheduled todo. Users will also be able to set SMS notifications for reminding them to plan their week / month / day, as well as for reminding them when they have a scheduled todo coming up.

---

## Planning
- Figma designs are available [here](https://www.figma.com/file/ona2QoEu6QzTcyffAervOy/Todolender?node-id=0%3A1&t=KPdD8o2qc6cbYQnZ-0).
- A basic system design is available [here](https://app.diagrams.net/#HMark-Cooper-Janssen-Vooles%2FTodolendar.API%2Fmaster%2FTodolendar%20System%20Design).
- A basic database schema is available [here](https://app.diagrams.net/#HMark-Cooper-Janssen-Vooles%2FTodolendar.API%2Fmaster%2FTodolendar.DB.Schema).

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

To expose the API in the browser, i've written a guide about it [here](https://github.com/Mark-Cooper-Janssen-Vooles/devops-webdev-guide#exposing-an-api-on-an-ec2).

However, when using CodeDeploy there is one more step. You cannot simply run `dotnet Todolendar.API.dll --urls "http://*:5000;https://*:5001"` because this will be a constantly running process, and the script will timeout. To fix this issue:
  - We need to run the application as a daemon process. This will help with restarts etc too 
  - This app has used pm2 to manage it as a background process which takes care of starting, stopping and restarting it 

Note that the EC2 is using a elastic IP address, which enables it to have the same IP address if restarted or rebooted. 

---

## Database 

A mySQL database is used in production. 

To set this up, I manually created an mysql database in RDS. 
- Used the free tier 
- Set a username and password
- Made sure the VPC was the same as the EC2
- After creating it, went into the security group for it and added an inbound rule with "custom TCP" with the port as 3306 (the port the sql db is open to), and put the source as the security group for the ec2 (i.e. it should start with sg-*)


### Test the connection is working in the ec2, use root user:
  - `sudo yum install mysql`
  - `mysql -h database-1.cq3pcc0prrl2.ap-southeast-2.rds.amazonaws.com -u admin -p`
    - you will be prompted to enter the password
  - to see inside the database for tables etc, run `show databases;`,  to see inside tables use `use todolendardb;` and `show tables;`
    - type `exit` to exit the mysql command line

### Set up migrations:
- From here, need to set up migration
- One way to do this is to run this command in the SSH connection (first cd to the .dll directory)
  - SSH as root user
  - install dotnet ef (add this to start_server.sh):
    - `dotnet tool install --global dotnet-ef`
    - `export PATH="$PATH:$HOME/.dotnet/tools"`
    - run this to make sure its installed: `dotnet ef --version`
  - cd into the api project. /api/Todolendar.API/Todolendar.API and run `dotnet ef migrations add InitialCreate`
- then actually run the migration: `dotnet ef database update`
- You should see a success message now. You can connect to the database again and check the tables are there. 
- This should be a once off when setting up the EC2. May need to do another migration if the models change etc. 

---

