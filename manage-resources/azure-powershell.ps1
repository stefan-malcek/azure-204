Import-Module Az

Connect-AzAccount

# Get active subscription
Get-AzContext

# List all subscriptions
Get-AzSubscription

# Select subscription
Select-AzSubscription -Subscription "<name>"

# List resource groups in table
Get-AzResourceGroup | Format-Table

# Create resource group
New-AzResourceGroup -Name <name> -Location <location>

# List resources with name filter 
Get-AzResource -ResourceGroupName ExerciseResources | ft

# Create a new VM
New-AzVm 
       -ResourceGroupName <resource group name> 
       -Name <machine name> 
       -Credential (Get-Credential) 
       -Location <location> 
       -Image <image name>
	   
New-AzVm -ResourceGroupName learn-24c276c8-4b61-4b51-a816-b1f551aaaf81 -Name "testvm-eus-01" -Credential (Get-Credential) -Location "East US" -Image UbuntuLTS -OpenPorts 22
	   
$vm = Get-AzVM -Name "testvm-eus-01" -ResourceGroupName learn-24c276c8-4b61-4b51-a816-b1f551aaaf81

# Pass VM object into other cmdlet
$vm | Get-AzPublicIpAddress
	   
# Interacting with vm in object way
$ResourceGroupName = "ExerciseResources"
$vm = Get-AzVM  -Name MyVM -ResourceGroupName $ResourceGroupName
$vm.HardwareProfile.vmSize = "Standard_DS3_v2"

Update-AzVM -ResourceGroupName $ResourceGroupName  -VM $vm

Stop-AzVM -Name $vm.Name -ResourceGroup $vm.ResourceGroupName

# Removing Vm does not clean disks or network interface
Remove-AzVM -Name $vm.Name -ResourceGroup $vm.ResourceGroupName

Get-AzResource -ResourceGroupName $vm.ResourceGroupName | ft

$vm | Remove-AzNetworkInterface –Force

# Delete managed OS disks and storage account
Get-AzDisk -ResourceGroupName $vm.ResourceGroupName -DiskName $vm.StorageProfile.OSDisk.Name | Remove-AzDisk -Force

# Delete virtual network
Get-AzVirtualNetwork -ResourceGroup $vm.ResourceGroupName | Remove-AzVirtualNetwork -Force

# Delete network security group
Get-AzNetworkSecurityGroup -ResourceGroup $vm.ResourceGroupName | Remove-AzNetworkSecurityGroup -Force

# Delete public IP address
Get-AzPublicIpAddress -ResourceGroup $vm.ResourceGroupName | Remove-AzPublicIpAddress -Force

Get-AzResource -ResourceType Microsoft.Compute/virtualMachines