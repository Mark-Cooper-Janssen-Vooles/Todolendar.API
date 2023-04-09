# Note: this is used in the EC2 as "user data" when creating an instance. 
# It installs the codedeploy daemon - codedeploy does not work without it.

# The EC2 is an amazon linux 2 t2.micro with a custom security group, custom VPC, custom IAM role, elastic IP address.

#!/bin/bash
yum update -y
yum install -y ruby
yum install -y aws-cli
cd /home/ec2-user
aws s3 cp s3://aws-codedeploy-ap-southeast-2/latest/install . --region ap-southeast-2
chmod +x ./install
./install auto