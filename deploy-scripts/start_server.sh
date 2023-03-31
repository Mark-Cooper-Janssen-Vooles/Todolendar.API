export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"  # This loads nvm
[ -s "$NVM_DIR/bash_completion" ] && \. "$NVM_DIR/bash_completion"

export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet

cd /home/ec2-user/api/Todolendar.API/publish

# Note: need to run API in pm2 as a demon service or else the process never finishes and codeDeploy gets a timeout 

# Note: need to use sudo to expose it on any port (the * part)
# By default, the sudo command resets the PATH environment variable to a secure path that doesn't include the path to the dotnet executable. To allow sudo to find the dotnet executable, you can use the full path to the dotnet executable

# sudo /home/ec2-user/dotnet/dotnet Todolendar.API.dll


pm2 start dotnet --name TodolendarAPI -- Todolendar.API.dll
