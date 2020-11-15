# Set an admin login and password for your database
export ADMINLOGIN='severAdmin'
export PASSWORD='Plassword1'
# Set the logical SQL server name. We'll add a random string as it needs to be globally unique.
export SERVERNAME=server$RANDOM
export RESOURCEGROUP=learn-4dea3db6-2ada-4914-ad75-90d85a494e5d
# Set the location, we'll pull the location from our resource group.
export LOCATION=$(az group show --name $RESOURCEGROUP | jq -r '.location')

az sql server create \
    --name $SERVERNAME \
    --resource-group $RESOURCEGROUP \
    --location $LOCATION \
    --admin-user $ADMINLOGIN \
    --admin-password "$PASSWORD"
	
az sql db create --resource-group $RESOURCEGROUP \
    --server $SERVERNAME \
    --name marketplaceDb \
    --sample-name AdventureWorksLT \
    --service-objective Basic
	
az sql db show-connection-string --client sqlcmd --name marketplaceDb --server $SERVERNAME | jq -r

sqlcmd -S tcp:server24343.database.windows.net,1433 -d marketplaceDb -U 'severAdmin' -P 'Plassword1' -N -l 30

az vm create \
  --resource-group $RESOURCEGROUP \
  --name appServer \
  --image UbuntuLTS \
  --size Standard_DS2_v2 \
  --generate-ssh-keys
  
ssh 157.56.162.80

echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev
