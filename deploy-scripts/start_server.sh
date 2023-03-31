export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"  # This loads nvm
[ -s "$NVM_DIR/bash_completion" ] && \. "$NVM_DIR/bash_completion"

export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet

cd /home/ec2-user/api/Todolendar.API/publish
# Note: need to run API in pm2 as a demon service or else the process never finishes and codeDeploy gets a timeout 
pm2 start dotnet --name todolendar -- Todolendar.API.dll --urls "http://*:5000;https://*:5001" --daemon

# in the EC2 you should be able to run: 'wget localhost:5000/ping --no-check-certificate' and then 'cat ping' and see output to confirm it is working.

# Note: if wanting to check without pm2, need to use sudo to expose it on any port (the * part)
# By default, the sudo command resets the PATH environment variable to a secure path that doesn't include the path to the dotnet executable. To allow sudo to find the dotnet executable, you can use the full path to the dotnet executable

# sudo /home/ec2-user/dotnet/dotnet Todolendar.API.dll --urls "http://*:5000;https://*:5001"