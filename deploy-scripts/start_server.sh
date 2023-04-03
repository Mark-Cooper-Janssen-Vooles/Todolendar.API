export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"  # This loads nvm
[ -s "$NVM_DIR/bash_completion" ] && \. "$NVM_DIR/bash_completion"

export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet

cd /home/ec2-user/api/Todolendar.API/publish

export ASPNETCORE_ENVIRONMENT=Production
pm2 start dotnet --name todolendar -- Todolendar.API.dll --urls "http://*:5000;https://*:5001" --daemon