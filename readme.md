<h1>Dynamic Configuration</h1>

### Description
This project has 3 parts: 
* Demo : Implementation of library
* Configuration : Crud operations for configs
* DynamicConfiguration : Class library for read configs

The demo application provides access to the library with a DLL. You can see it under the library folder in the demo app. 
### Usage

To run the project, you can follow the steps below.

1. Clone project
> https://github.com/hkubrakurnaz/DynamicConfiguration
2. Run docker compose file
> docker-compose up -d

Two different applications will be launched: Demo and ConfigurationApi.

The demo application will be launched on this url:
> http://localhost:8080/swagger/index.html

The Configuration api will be launched on this url:
> http://localhost:8081/swagger/index.html

Note: Collection and data will be created automatically.
