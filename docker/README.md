# Cammad Docker Setup (Alternative UI for Camunda / Replacement for Camunda Webapps)

**This is a development version for demo purposes!**

You must access the UI from the root url.  
If you copy a url with path or query parameters the page will fail to load.

## Setup:

1. Download the Cammand.zip from the Releases and uzip into the `docker/cammand` folder.
1. Add your `keystore.p12` or pcks file for https into the root of the docker folder.
1. Update the `default.yml` for the ssl settings:
   ```yml
    server.ssl.key-store: classpath:keystore.p12
    server.ssl.key-store-password: 1234567890
    server.ssl.key-store-type: PKCS12
    server.ssl.key-alias: tomcat
   ```
1. run from the docker folder: `docker-compose up`
1. `https://locahost:8080`  (note **s** in the https)

If you want to access the original Camunda WebApps:

1. `localhost:8080/camunda/app/welcome`
1. `localhost:8080/camunda/app/cockpit`
1. `localhost:8080/camunda/app/tasklist`
1. `localhost:8080/camunda/app/admin`