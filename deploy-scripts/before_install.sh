# guide followed here: https://www.bluelabellabs.com/blog/how-to-setup-aws-codepipeline-for-a-asp-net-core-web-api-project/

# install nvm
echo "installing nvm"
curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.3/install.sh | bash
. ~/.nvm/nvm.sh
export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"  # This loads nvm

echo "installing node 16"
nvm install 16

# install PM2 (process management)
echo "installing pm2"
npm install pm2 -g

# sudo yum -y install libicu60

# install .net runtime - https://learn.microsoft.com/en-us/dotnet/core/install/
cd ~
wget https://download.visualstudio.microsoft.com/download/pr/868b2f38-62ca-4fd8-93ea-e640cf4d2c5b/1e615b6044c0cf99806b8f6e19c97e03/dotnet-sdk-6.0.407-linux-x64.tar.gz
mkdir -p $HOME/dotnet && tar zxf ~/dotnet-sdk-6.0.407-linux-x64.tar.gz -C $HOME/dotnet
export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet

echo 'export DOTNET_ROOT=$HOME/dotnet' >> .bash_profile
echo 'export PATH=$PATH:$HOME/dotnet' >> .bash_profile
