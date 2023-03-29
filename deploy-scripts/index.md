## Deploy Scripts 

These scripts will be used by CodeDeploy in aws, from the appspec.yml file. 

To check the app is running correctly:
- connect to the EC2 and run `pm2 list` and it should show 'todolendar' as 'online' status. 
- curl an endpoint 