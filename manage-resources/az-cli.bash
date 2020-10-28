az login

export RESOURCE_GROUP=learn-62ce1a83-805b-4a3d-952c-573e50b83bcf
export AZURE_REGION=westeu
export AZURE_APP_PLAN=popupappplan-$RANDOM
export AZURE_WEB_APP=popupwebapp-$RANDOM

# List resource groups
az group list --output table

# Query
az group list --query "[?name == '$RESOURCE_GROUP']"

# Create an App Service plan 
az appservice plan create --name $AZURE_APP_PLAN --resource-group $RESOURCE_GROUP --location $AZURE_REGION --sku FREE

# Verify that the service plan was created successfully
az appservice plan list --output table

# Create the web app
az webapp create --name $AZURE_WEB_APP --resource-group $RESOURCE_GROUP --plan $AZURE_APP_PLAN

# Verify that the app was created successfully
az webapp list --output table

# Deploy code from a GitHub repository to the web app
az webapp deployment source config --name $AZURE_WEB_APP --resource-group $RESOURCE_GROUP --repo-url "https://github.com/Azure-Samples/php-docs-hello-world" --branch master --manual-integration