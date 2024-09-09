metadata description = 'Creates an SignalR Services instance.'
param name string
param location string = resourceGroup().location
param tags object = {}
param disableLocalAuth bool = true

param sku object = {
  name: 'Premium_P1'
}

resource signalr 'Microsoft.SignalRService/signalR@2023-08-01-preview' = {
  name: name
  location: location
  tags: tags
  sku: sku
  properties: {
    disableLocalAuth: disableLocalAuth
  }
}

output endpoint string = signalr.properties.hostName
output id string = signalr.id
output name string = signalr.name
