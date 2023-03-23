export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"  # This loads nvm
[ -s "$NVM_DIR/bash_completion" ] && \. "$NVM_DIR/bash_completion"

echo "nvm ls"
echo "npm -v" 

cd /home/ec2-user/api/Todolendar.API/publish
pm2 start "Todolendar.API.dll" --name todolendar