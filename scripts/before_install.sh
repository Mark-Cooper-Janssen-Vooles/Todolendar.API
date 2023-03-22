# guide followed here: https://www.bluelabellabs.com/blog/how-to-setup-aws-codepipeline-for-a-asp-net-core-web-api-project/

# install nvm
curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.3/install.sh | bash
. ~/.nvm/nvm.sh
nvm install 16

# install PM2 (process management)
npm install pm2 -g

# install .net runtime - https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual#scripted-install
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
sudo chmod +x ./dotnet-install.sh
./dotnet-install.sh --version latest --runtime dotnet
