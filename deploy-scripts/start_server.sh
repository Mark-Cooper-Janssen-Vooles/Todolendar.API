export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"  # This loads nvm
[ -s "$NVM_DIR/bash_completion" ] && \. "$NVM_DIR/bash_completion"

cd /home/ec2-user/api/Todolendar.API/publish
# pm2 start "Todolendar.API.dll" --name todolendar

# sudo dotnet Todolendar.API.dll --urls http://0.0.0.0:80


# might need to pass in env variable here to get env to production? 