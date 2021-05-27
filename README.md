# Cammand

Alternative UI to Camunda BPM Webapps (Tasklist, Cockpit, and Admin) + More!


WIP.

Contributions and collaboration is always welcomed.


1. Extendable
2. Customizable
3. Enterprise Friendly
4. White-label Friendly
5. Theme-able
6. Runtime and History API access!
7. Swap APIs for your custom endpoints
8. What more do you need?


![Process Instance](./docs/images/Process-Instance-Info1.png)

![Lower Process Instance](./docs/images/Process-Instance-Info2.png)

![List of Process Definitions](./docs/images/Process-Definitions1.png)



# Internals

1. Built with Microsoft Blazor Web Assembly.
1. Supports Form.io Form Rendering.
1. Supports BPMN.io rendering.
1. Supports Camunda API using Camunda's OpenAPI support.



# BPMN.io / bpmnjs Support:

Notice the ability to define custom Render Fragments (HTML) for overlays and other common modifications!

```xml
<Column>
                <Card>
                    <CardHeader>BPMN Diagram</CardHeader>
                    <CardBody Style="height: 700px">
                        <BpmnViewer bpmnXml="@bpmnXml"
                                    activityIncidentCounts="@activityIncidentCounts"
                                    activeActivityCounts="@activeActivityCounts"
                                    completedActivityInstanceCounts="@completedActivityInstanceCounts"
                                    applyCompletedActivityMarker="true"
                                    completedActivityMarkerConfig="highlight">
                            <activityIncidentCountDisplay>
                                <Badge Color="Color.Danger">@context</Badge>
                            </activityIncidentCountDisplay>
                            <activeActivityCountDisplay>
                                <Badge Color="Color.Primary">@context</Badge>
                            </activeActivityCountDisplay>
                            <completedActivityInstanceCountDisplay>
                                <Badge Color="Color.Success">@context</Badge>
                            </completedActivityInstanceCountDisplay>
                        </BpmnViewer>
                    </CardBody>
                </Card>
            </Column>
```

