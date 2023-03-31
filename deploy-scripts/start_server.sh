export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"  # This loads nvm
[ -s "$NVM_DIR/bash_completion" ] && \. "$NVM_DIR/bash_completion"

export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet

cd /home/ec2-user/api/Todolendar.API/publish

# note need to use sudo, and need to point it to where the .net executable is
# By default, the sudo command resets the PATH environment variable to a secure path that doesn't include the path to the dotnet executable. To allow sudo to find the dotnet executable, you can use the full path to the dotnet executable
sudo /home/ec2-user/dotnet/dotnet Todolendar.API.dll --urls "http://*:5000;https://*5001"
