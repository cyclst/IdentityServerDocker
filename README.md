# IdentityServerDocker
Duende IdentityServer Quickstart With Container Orchestration

This repo takes the quickstart examples from https://github.com/DuendeSoftware/Samples and configures them to work with Docker Container Orchestration on a local machine (Docker Desktop).

Some changes were required to the original quickstart files, but I have tried to keep these to a minimum. These were done using Visual Studion Community Edition 2022.

Please note: These changes made are the minimum effort to get the quickstarts working in docker desktop for local development and are not suitable for production. I have decorated changes with if(Environment.IdDevelopment()) where possible.

The high level overview of these changes are as follows:

1. Add Contaner Orchestration Support in each project
2. Replace the hard coded urls in code with values defined in appsettings configuration
3. Override appsettings configuration from docker-compose.override with docker targeted values
4. Set cookie options required for local development

Gotchas:
1. I have created a env.js file for the JavaScriptClient to get its variables from, which I then override in docker-compose by mounting a docker target variable file (env.docker.js) in its place. However, because VS uses "FastMode" it serves files mounted directly from the filesystem to enable debugging, which means the docker variable file does not get served in the browser. To get around this run docker-compose in VS in Release mode, rather than Debug. This disabled FastMode. Alternatively just run docker compose from the command line ;-) 
2. The Duende quickstart examples do not seem to have Single Signon (SSO) and Single Logout (SLO) working between the WebClient and JavaScriptClient. I have not attempted to fix this as it beyond the scope of this POC. One for another day!
