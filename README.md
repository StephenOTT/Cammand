# Cammand

Alternative UI to Camunda BPM Webapps (Tasklist, Cockpit, and Admin) + More!

Default app is a "Kitchen Sick" app that covers runtime and history apis.

Contributions and collaboration is always welcomed.

## Features:

1. Extendable
2. Customizable
3. Enterprise Friendly
4. White-label Friendly
5. Theme-able
6. Runtime and History API access!
7. Swap APIs for your custom endpoints
8. What more do you need?

## Screenshots

### Process Definitions

![Process Instance](./docs/images/Mgmt-Definitions.png)
![Process Instance](./docs/images/Mgmt-Definitions-Selected.png)
![Process Instance](./docs/images/Mgmt-Definition-Details.png)
![Process Instance](./docs/images/Mgmt-Definition-Details-Bpmn-Zoom.png)
![Process Instance](./docs/images/Mgmt-ProcessDefinition-SuspensionAction-1.png)
![Process Instance](./docs/images/Mgmt-ProcessDefinition-SuspensionAction-2.png)
![Process Instance](./docs/images/Mgmt-Definition-HistoryTtl-Update.png)
![Process Instance](./docs/images/Mgmt-Definitions-Delete.png)


### Deployments and Forms

![Process Instance](./docs/images/Mgmt-CreateDeployment.png)
![Process Instance](./docs/images/Mgmt-Deployments.png)
![Process Instance](./docs/images/Mgmt-FormBuilder.png)


### Process Instances
![Process Instance](./docs/images/Mgmt-ProcessInstance-Details-Variables.png)
![Process Instance](./docs/images/Mgmt-ProcessInstance-Details-Variables-json.png)
![Process Instance](./docs/images/Mgmt-ProcessInstance-Details-Incidents.png)
![Process Instance](./docs/images/Mgmt-ProcessInstance-Details-UserTasks.png)
![Process Instance](./docs/images/Mgmt-ProcessInstance-Details-Jobs.png)
![Process Instance](./docs/images/Mgmt-ProcessInstance-Details-ExternalTasks.png)

### Start a Process and Tasklist
![Process Instance](./docs/images/Mgmt-Startable-Definitions.png)
![Process Instance](./docs/images/Tasklist-MyTasks.png)

### Jobs
![Jobs](./docs/images/Mgmt-Jobs.png)

## Quick Start

1. open terminal at `./src/MainApp`
1. run `dotnet run`
1. got to `localhost:5001`

Currently requires HTTPS on the Camunda API endpoint.



## Quick SpringBoot Configs for Camunda:

Development use only.

```kotlin
@Configuration
class CamundaConfig {

    @Bean
    fun processCorsFilter(): FilterRegistrationBean<*> {
        val source = UrlBasedCorsConfigurationSource()
        val config = CorsConfiguration()
        config.allowCredentials = true
        config.addAllowedOrigin("https://localhost:5001")
        config.addAllowedHeader("*")
        config.addAllowedMethod("*")
        source.registerCorsConfiguration("/**", config)

        val bean = FilterRegistrationBean(CorsFilter(source))
        bean.order = 0
        return bean
    }
}

@Configuration
class CamundaSecurityFilter {
    @Bean
    fun processEngineAuthenticationFilter(): FilterRegistrationBean<*> {
        val registration = FilterRegistrationBean<Filter>()
        registration.setName("camunda-auth")
        registration.filter = getProcessEngineAuthenticationFilter()
        registration.addInitParameter(
            "authentication-provider",
            "org.camunda.bpm.engine.rest.security.auth.impl.HttpBasicAuthenticationProvider"
        )
        registration.addUrlPatterns("/engine-rest/*")
        return registration
    }

    @Bean
    fun getProcessEngineAuthenticationFilter(): Filter {
        return ProcessEngineAuthenticationFilter()
    }
}
```

application.yml

```yml
server.ssl.key-store: classpath:keystore.p12
server.ssl.key-store-password: MYPASSWORD
server.ssl.key-store-type: PKCS12
server.ssl.key-alias: tomcat
```