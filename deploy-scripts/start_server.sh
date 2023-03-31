export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"  # This loads nvm
[ -s "$NVM_DIR/bash_completion" ] && \. "$NVM_DIR/bash_completion"

export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet

cd /home/ec2-user/api/Todolendar.API/publish

dotnet Todolendar.API.dll --urls "http://*:5000;https://*5001"
