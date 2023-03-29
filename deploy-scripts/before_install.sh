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

# install .net runtime - https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual#scripted-install
# echo "installing .net runtime"
# sudo wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
# sudo chmod +x ./dotnet-install.sh
# ./dotnet-install.sh --version latest --runtime dotnet

sudo wget https://download.visualstudio.microsoft.com/download/pr/868b2f38-62ca-4fd8-93ea-e640cf4d2c5b/1e615b6044c0cf99806b8f6e19c97e03/dotnet-sdk-6.0.407-linux-x64.tar.gz
DOTNET_FILE=dotnet-sdk-6.0.407-linux-x64.tar.gz
export DOTNET_ROOT=$(pwd)/.dotnet

mkdir -p "$DOTNET_ROOT" && tar zxf "$DOTNET_FILE" -C "$DOTNET_ROOT"

export PATH=$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools

export DOTNET_ROOT=$HOME/.dotnet
export PATH=$PATH:$HOME/.dotnet:$HOME/.dotnet/tools

# mkdir -p $HOME/dotnet && tar zxf ~/dotnet-sdk-3.1.302-linux-arm64.tar.gz -C $HOME/dotnet
# export DOTNET_ROOT=$HOME/dotnet