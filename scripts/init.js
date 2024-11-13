db = db.getSiblingDB('configurationDb');
db.createCollection('configurations');
db.configurations.insertOne({
    "name": "SiteName",
    "type": "String",
    "value": "ConfigValue",
    "isActive": true,
    "applicationName": "Demo"
});

db.configurations.insertOne({
    "name": "IsBasketEnabled",
    "type": "Boolean",
    "value": "true",
    "isActive": true,
    "applicationName": "Demo"
});

db.configurations.insertOne({
    "name": "MaxItemCount",
    "type": "Int",
    "value": "50",
    "isActive": true,
    "applicationName": "Demo"
});
