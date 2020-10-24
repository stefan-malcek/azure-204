# Create a virtual machine.
az vm create \
  --resource-group learn-73aa3174-68af-4ac0-bf8f-42dd9ed57dca \
  --name MeanStack \
  --image Canonical:UbuntuServer:16.04-LTS:latest \
  --admin-username azureuser \
  --generate-ssh-keys
  
# Open HTTP port on the virtual machine.
az vm open-port \
  --port 80 \
  --resource-group learn-73aa3174-68af-4ac0-bf8f-42dd9ed57dca \
  --name MeanStack
  
ipaddress=$(az vm show \
  --name MeanStack \
  --resource-group learn-73aa3174-68af-4ac0-bf8f-42dd9ed57dca \
  --show-details \
  --query [publicIps] \
  --output tsv)
  
# Connect to the virtual machine.
ssh azureuser@$ipaddress

# Update the package manager.
sudo apt update && sudo apt upgrade -y

# Install MongoDB
sudo apt-get install -y mongodb

# Check whether mongo service is running.
sudo systemctl status mongodb

<<SERVICE_RUNNING_LOG
azureuser@MeanStack:~$ sudo systemctl status mongodb
● mongodb.service - An object/document-oriented database
  Loaded: loaded (/lib/systemd/system/mongodb.service; enabled; vendor preset: enabled)
  Active: active (running) since Thu 2019-08-22 16:46:30 UTC; 9s ago
    Docs: man:mongod(1)
Main PID: 18360 (mongod)
  CGroup: /system.slice/mongodb.service
          └─18360 /usr/bin/mongod --config /etc/mongodb.conf

Aug 22 16:46:30 MeanStack systemd[1]: Started An object/document-oriented database.
SERVICE_RUNNING_LOG

# Run Mongo version command to verify the installation.
mongod --version

# Register the Node.js repository.
curl -sL https://deb.nodesource.com/setup_8.x | sudo -E bash -

# Install the Node.js package.
sudo apt-get install -y nodejs

# Run Node version command to verify the installation.
nodejs -v

# Copy files to VM
scp -r ~/Books azureuser@$ipaddress:~/Books

# Print content of variable
echo $ipaddress

ssh azureuser@$ipaddress

cd ~/Books

# Install packages.
npm install

# Run server.
sudo node server.js