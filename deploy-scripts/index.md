## Deploy Scripts 

These scripts will be used by CodeDeploy in aws, from the appspec.yml file. 

To check the app is running correctly:
- connect to the EC2 and run `pm2 list` and it should show 'todolendar' as 'online' status. 
- hit an endpoint `wget localhost:5000/ping --no-check-certificate` and then `cat ping` and see output to confirm it is working
- If wanting to check how the dll runs without pm2, need to use sudo to expose it on any port (the * part)
  - By default, the sudo command resets the PATH environment variable to a secure path that doesn't include the path to the dotnet executable. To allow sudo to find the dotnet executable, you can use the full path to the dotnet executable
  - `sudo /home/ec2-user/dotnet/dotnet Todolendar.API.dll --urls "http://*:5000;https://*:5001"`