#nullable enable
using System;
using System.Net.Http;
using Camunda.Http.Api.Api;
using Camunda.Http.Api.Client;

namespace Camunda.Http.Api
{
    public class CamundaClient
    {
        private Configuration Configuration { get; } = new();
        private HttpClient HttpClient { get; } = new();
        private HttpClientHandler HttpClientHandler { get; } = new();

        public CamundaApi Api { get; private set; }


        /// <summary>
        /// Setup client with defaults
        /// </summary>
        public CamundaClient()
        {
            Api = BuildClient();
        }

        /// <summary>
        /// Setup client with custom config
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="httpClient"></param>
        /// <param name="httpClientHandler"></param>
        public CamundaClient(Configuration configuration, HttpClient httpClient, HttpClientHandler httpClientHandler)
        {
            Configuration = configuration;
            HttpClient = httpClient;
            HttpClientHandler = httpClientHandler;
            Api = BuildClient();
        }

        /// <summary>
        /// Create instance for use with Basic Auth
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="httpClient"></param>
        /// <param name="httpClientHandler"></param>
        /// <param name="username">Basic Auth Username.  Will override default headers in Configuration</param>
        /// <param name="password">Basic Auth Password. Will override default headers in Configuration</param>
        public CamundaClient(Configuration configuration, HttpClient httpClient, HttpClientHandler httpClientHandler,
            string username, string password)
        {
            Configuration = configuration;
            Configuration.DefaultHeaders.Add("Authorization",
                $"Basic " + ClientUtils.Base64Encode($"{username}:{password}"));
            HttpClient = httpClient;
            HttpClientHandler = httpClientHandler;
            Api = BuildClient();
        }

        private CamundaApi BuildClient()
        {
            if (Configuration == null) throw new ArgumentException("Configuration cannot be null.");
            if (HttpClient == null) throw new ArgumentException("HttpClient cannot be null.");
            if (HttpClientHandler == null) throw new ArgumentException("HttpClientHandler cannot be null.");

            return new CamundaApi(
                new Lazy<IAuthorizationApi>(() => new AuthorizationApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IBatchApi>(() => new BatchApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IConditionApi>(() => new ConditionApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IDecisionDefinitionApi>(() =>
                    new DecisionDefinitionApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IDecisionRequirementsDefinitionApi>(() =>
                    new DecisionRequirementsDefinitionApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IDeploymentApi>(() => new DeploymentApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IEngineApi>(() => new EngineApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IEventSubscriptionApi>(() =>
                    new EventSubscriptionApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IExecutionApi>(() => new ExecutionApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IExternalTaskApi>(() => new ExternalTaskApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IFilterApi>(() => new FilterApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IGroupApi>(() => new GroupApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricActivityInstanceApi>(() =>
                    new HistoricActivityInstanceApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricBatchApi>(() => new HistoricBatchApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricDecisionDefinitionApi>(() =>
                    new HistoricDecisionDefinitionApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricDecisionRequirementsDefinitionApi>(() =>
                    new HistoricDecisionRequirementsDefinitionApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricDetailApi>(() => new HistoricDetailApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricExternalTaskLogApi>(() =>
                    new HistoricExternalTaskLogApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricIdentityLinkLogApi>(() =>
                    new HistoricIdentityLinkLogApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricIncidentApi>(() =>
                    new HistoricIncidentApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricJobLogApi>(() => new HistoricJobLogApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricProcessDefinitionApi>(() =>
                    new HistoricProcessDefinitionApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricProcessInstanceApi>(() =>
                    new HistoricProcessInstanceApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricTaskInstanceApi>(() =>
                    new HistoricTaskInstanceApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricUserOperationLogApi>(() =>
                    new HistoricUserOperationLogApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoricVariableInstanceApi>(() =>
                    new HistoricVariableInstanceApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IHistoryCleanupApi>(() => new HistoryCleanupApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IIdentityApi>(() => new IdentityApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IIncidentApi>(() => new IncidentApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IJobApi>(() => new JobApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IJobDefinitionApi>(() => new JobDefinitionApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IMessageApi>(() => new MessageApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IMetricsApi>(() => new MetricsApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IMigrationApi>(() => new MigrationApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IModificationApi>(() => new ModificationApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IProcessDefinitionApi>(() =>
                    new ProcessDefinitionApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IProcessInstanceApi>(
                    () => new ProcessInstanceApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<ISchemaLogApi>(() => new SchemaLogApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<ISignalApi>(() => new SignalApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<ITaskApi>(() => new TaskApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<ITaskAttachmentApi>(() => new TaskAttachmentApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<ITaskCommentApi>(() => new TaskCommentApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<ITaskIdentityLinkApi>(() =>
                    new TaskIdentityLinkApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<ITaskLocalVariableApi>(() =>
                    new TaskLocalVariableApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<ITaskVariableApi>(() => new TaskVariableApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<ITelemetryApi>(() => new TelemetryApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<ITenantApi>(() => new TenantApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IUserApi>(() => new UserApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IVariableInstanceApi>(() =>
                    new VariableInstanceApi(HttpClient, Configuration, HttpClientHandler)),
                new Lazy<IVersionApi>(() => new VersionApi(HttpClient, Configuration, HttpClientHandler))
            );
        }

        public class CamundaApi
        {
            public CamundaApi(Lazy<IAuthorizationApi> authorizationApi, Lazy<IBatchApi> batchApi,
                Lazy<IConditionApi> conditionApi, Lazy<IDecisionDefinitionApi> decisionDefinitionApi,
                Lazy<IDecisionRequirementsDefinitionApi> decisionRequirementsDefinitionApi,
                Lazy<IDeploymentApi> deploymentApi, Lazy<IEngineApi> engineApi,
                Lazy<IEventSubscriptionApi> eventSubscriptionApi, Lazy<IExecutionApi> executionApi,
                Lazy<IExternalTaskApi> externalTaskApi, Lazy<IFilterApi> filterApi, Lazy<IGroupApi> groupApi,
                Lazy<IHistoricActivityInstanceApi> historicActivityInstanceApi,
                Lazy<IHistoricBatchApi> historicBatchApi,
                Lazy<IHistoricDecisionDefinitionApi> historicDecisionDefinitionApi,
                Lazy<IHistoricDecisionRequirementsDefinitionApi> historicDecisionRequirementsDefinitionApi,
                Lazy<IHistoricDetailApi> historicDetailApi,
                Lazy<IHistoricExternalTaskLogApi> historicExternalTaskLogApi,
                Lazy<IHistoricIdentityLinkLogApi> historicIdentityLinkLogApi,
                Lazy<IHistoricIncidentApi> historicIncidentApi, Lazy<IHistoricJobLogApi> historicJobLogApi,
                Lazy<IHistoricProcessDefinitionApi> historicProcessDefinitionApi,
                Lazy<IHistoricProcessInstanceApi> historicProcessInstanceApi,
                Lazy<IHistoricTaskInstanceApi> historicTaskInstanceApi,
                Lazy<IHistoricUserOperationLogApi> historicUserOperationLogApi,
                Lazy<IHistoricVariableInstanceApi> historicVariableInstanceApi,
                Lazy<IHistoryCleanupApi> historyCleanupApi, Lazy<IIdentityApi> identityApi,
                Lazy<IIncidentApi> incidentApi, Lazy<IJobApi> jobApi, Lazy<IJobDefinitionApi> jobDefinitionApi,
                Lazy<IMessageApi> messageApi, Lazy<IMetricsApi> metricsApi, Lazy<IMigrationApi> migrationApi,
                Lazy<IModificationApi> modificationApi, Lazy<IProcessDefinitionApi> processDefinitionApi,
                Lazy<IProcessInstanceApi> processInstanceApi, Lazy<ISchemaLogApi> schemaLogApi,
                Lazy<ISignalApi> signalApi, Lazy<ITaskApi> taskApi, Lazy<ITaskAttachmentApi> taskAttachmentApi,
                Lazy<ITaskCommentApi> taskCommentApi, Lazy<ITaskIdentityLinkApi> taskIdentityLinkApi,
                Lazy<ITaskLocalVariableApi> taskLocalVariableApi, Lazy<ITaskVariableApi> taskVariableApi,
                Lazy<ITelemetryApi> telemetryApi, Lazy<ITenantApi> tenantApi, Lazy<IUserApi> userApi,
                Lazy<IVariableInstanceApi> variableInstanceApi, Lazy<IVersionApi> versionApi)
            {
                _authorizationApi = authorizationApi;
                _batchApi = batchApi;
                _conditionApi = conditionApi;
                _decisionDefinitionApi = decisionDefinitionApi;
                _decisionRequirementsDefinitionApi = decisionRequirementsDefinitionApi;
                _deploymentApi = deploymentApi;
                _engineApi = engineApi;
                _eventSubscriptionApi = eventSubscriptionApi;
                _executionApi = executionApi;
                _externalTaskApi = externalTaskApi;
                _filterApi = filterApi;
                _groupApi = groupApi;
                _historicActivityInstanceApi = historicActivityInstanceApi;
                _historicBatchApi = historicBatchApi;
                _historicDecisionDefinitionApi = historicDecisionDefinitionApi;
                _historicDecisionRequirementsDefinitionApi = historicDecisionRequirementsDefinitionApi;
                _historicDetailApi = historicDetailApi;
                _historicExternalTaskLogApi = historicExternalTaskLogApi;
                _historicIdentityLinkLogApi = historicIdentityLinkLogApi;
                _historicIncidentApi = historicIncidentApi;
                _historicJobLogApi = historicJobLogApi;
                _historicProcessDefinitionApi = historicProcessDefinitionApi;
                _historicProcessInstanceApi = historicProcessInstanceApi;
                _historicTaskInstanceApi = historicTaskInstanceApi;
                _historicUserOperationLogApi = historicUserOperationLogApi;
                _historicVariableInstanceApi = historicVariableInstanceApi;
                _historyCleanupApi = historyCleanupApi;
                _identityApi = identityApi;
                _incidentApi = incidentApi;
                _jobApi = jobApi;
                _jobDefinitionApi = jobDefinitionApi;
                _messageApi = messageApi;
                _metricsApi = metricsApi;
                _migrationApi = migrationApi;
                _modificationApi = modificationApi;
                _processDefinitionApi = processDefinitionApi;
                _processInstanceApi = processInstanceApi;
                _schemaLogApi = schemaLogApi;
                _signalApi = signalApi;
                _taskApi = taskApi;
                _taskAttachmentApi = taskAttachmentApi;
                _taskCommentApi = taskCommentApi;
                _taskIdentityLinkApi = taskIdentityLinkApi;
                _taskLocalVariableApi = taskLocalVariableApi;
                _taskVariableApi = taskVariableApi;
                _telemetryApi = telemetryApi;
                _tenantApi = tenantApi;
                _userApi = userApi;
                _variableInstanceApi = variableInstanceApi;
                _versionApi = versionApi;
            }

            private readonly Lazy<IAuthorizationApi> _authorizationApi;
            private readonly Lazy<IBatchApi> _batchApi;
            private readonly Lazy<IConditionApi> _conditionApi;
            private readonly Lazy<IDecisionDefinitionApi> _decisionDefinitionApi;
            private readonly Lazy<IDecisionRequirementsDefinitionApi> _decisionRequirementsDefinitionApi;
            private readonly Lazy<IDeploymentApi> _deploymentApi;
            private readonly Lazy<IEngineApi> _engineApi;
            private readonly Lazy<IEventSubscriptionApi> _eventSubscriptionApi;
            private readonly Lazy<IExecutionApi> _executionApi;
            private readonly Lazy<IExternalTaskApi> _externalTaskApi;
            private readonly Lazy<IFilterApi> _filterApi;
            private readonly Lazy<IGroupApi> _groupApi;
            private readonly Lazy<IHistoricActivityInstanceApi> _historicActivityInstanceApi;
            private readonly Lazy<IHistoricBatchApi> _historicBatchApi;
            private readonly Lazy<IHistoricDecisionDefinitionApi> _historicDecisionDefinitionApi;

            private readonly Lazy<IHistoricDecisionRequirementsDefinitionApi>
                _historicDecisionRequirementsDefinitionApi;

            private readonly Lazy<IHistoricDetailApi> _historicDetailApi;
            private readonly Lazy<IHistoricExternalTaskLogApi> _historicExternalTaskLogApi;
            private readonly Lazy<IHistoricIdentityLinkLogApi> _historicIdentityLinkLogApi;
            private readonly Lazy<IHistoricIncidentApi> _historicIncidentApi;
            private readonly Lazy<IHistoricJobLogApi> _historicJobLogApi;
            private readonly Lazy<IHistoricProcessDefinitionApi> _historicProcessDefinitionApi;
            private readonly Lazy<IHistoricProcessInstanceApi> _historicProcessInstanceApi;
            private readonly Lazy<IHistoricTaskInstanceApi> _historicTaskInstanceApi;
            private readonly Lazy<IHistoricUserOperationLogApi> _historicUserOperationLogApi;
            private readonly Lazy<IHistoricVariableInstanceApi> _historicVariableInstanceApi;
            private readonly Lazy<IHistoryCleanupApi> _historyCleanupApi;
            private readonly Lazy<IIdentityApi> _identityApi;
            private readonly Lazy<IIncidentApi> _incidentApi;
            private readonly Lazy<IJobApi> _jobApi;
            private readonly Lazy<IJobDefinitionApi> _jobDefinitionApi;
            private readonly Lazy<IMessageApi> _messageApi;
            private readonly Lazy<IMetricsApi> _metricsApi;
            private readonly Lazy<IMigrationApi> _migrationApi;
            private readonly Lazy<IModificationApi> _modificationApi;
            private readonly Lazy<IProcessDefinitionApi> _processDefinitionApi;
            private readonly Lazy<IProcessInstanceApi> _processInstanceApi;
            private readonly Lazy<ISchemaLogApi> _schemaLogApi;
            private readonly Lazy<ISignalApi> _signalApi;
            private readonly Lazy<ITaskApi> _taskApi;
            private readonly Lazy<ITaskAttachmentApi> _taskAttachmentApi;
            private readonly Lazy<ITaskCommentApi> _taskCommentApi;
            private readonly Lazy<ITaskIdentityLinkApi> _taskIdentityLinkApi;
            private readonly Lazy<ITaskLocalVariableApi> _taskLocalVariableApi;
            private readonly Lazy<ITaskVariableApi> _taskVariableApi;
            private readonly Lazy<ITelemetryApi> _telemetryApi;
            private readonly Lazy<ITenantApi> _tenantApi;
            private readonly Lazy<IUserApi> _userApi;
            private readonly Lazy<IVariableInstanceApi> _variableInstanceApi;
            private readonly Lazy<IVersionApi> _versionApi;

            public IAuthorizationApi AuthorizationApi => _authorizationApi.Value;
            public IBatchApi BatchApi => _batchApi.Value;
            public IConditionApi ConditionApi => _conditionApi.Value;
            public IDecisionDefinitionApi DecisionDefinitionApi => _decisionDefinitionApi.Value;

            public IDecisionRequirementsDefinitionApi DecisionRequirementsDefinitionApi =>
                _decisionRequirementsDefinitionApi.Value;

            public IDeploymentApi DeploymentApi => _deploymentApi.Value;
            public IEngineApi EngineApi => _engineApi.Value;
            public IEventSubscriptionApi EventSubscriptionApi => _eventSubscriptionApi.Value;
            public IExecutionApi ExecutionApi => _executionApi.Value;
            public IExternalTaskApi ExternalTaskApi => _externalTaskApi.Value;
            public IFilterApi FilterApi => _filterApi.Value;
            public IGroupApi GroupApi => _groupApi.Value;
            public IHistoricActivityInstanceApi HistoricActivityInstanceApi => _historicActivityInstanceApi.Value;
            public IHistoricBatchApi HistoricBatchApi => _historicBatchApi.Value;
            public IHistoricDecisionDefinitionApi HistoricDecisionDefinitionApi => _historicDecisionDefinitionApi.Value;

            public IHistoricDecisionRequirementsDefinitionApi HistoricDecisionRequirementsDefinitionApi =>
                _historicDecisionRequirementsDefinitionApi.Value;

            public IHistoricDetailApi HistoricDetailApi => _historicDetailApi.Value;
            public IHistoricExternalTaskLogApi HistoricExternalTaskLogApi => _historicExternalTaskLogApi.Value;
            public IHistoricIdentityLinkLogApi HistoricIdentityLinkLogApi => _historicIdentityLinkLogApi.Value;
            public IHistoricIncidentApi HistoricIncidentApi => _historicIncidentApi.Value;
            public IHistoricJobLogApi HistoricJobLogApi => _historicJobLogApi.Value;
            public IHistoricProcessDefinitionApi HistoricProcessDefinitionApi => _historicProcessDefinitionApi.Value;
            public IHistoricProcessInstanceApi HistoricProcessInstanceApi => _historicProcessInstanceApi.Value;
            public IHistoricTaskInstanceApi HistoricTaskInstanceApi => _historicTaskInstanceApi.Value;
            public IHistoricUserOperationLogApi HistoricUserOperationLogApi => _historicUserOperationLogApi.Value;
            public IHistoricVariableInstanceApi HistoricVariableInstanceApi => _historicVariableInstanceApi.Value;
            public IHistoryCleanupApi HistoryCleanupApi => _historyCleanupApi.Value;
            public IIdentityApi IdentityApi => _identityApi.Value;
            public IIncidentApi IncidentApi => _incidentApi.Value;
            public IJobApi JobApi => _jobApi.Value;
            public IJobDefinitionApi JobDefinitionApi => _jobDefinitionApi.Value;
            public IMessageApi MessageApi => _messageApi.Value;
            public IMetricsApi MetricsApi => _metricsApi.Value;
            public IMigrationApi MigrationApi => _migrationApi.Value;
            public IModificationApi ModificationApi => _modificationApi.Value;
            public IProcessDefinitionApi ProcessDefinitionApi => _processDefinitionApi.Value;
            public IProcessInstanceApi ProcessInstanceApi => _processInstanceApi.Value;
            public ISchemaLogApi SchemaLogApi => _schemaLogApi.Value;
            public ISignalApi SignalApi => _signalApi.Value;
            public ITaskApi TaskApi => _taskApi.Value;
            public ITaskAttachmentApi TaskAttachmentApi => _taskAttachmentApi.Value;
            public ITaskCommentApi TaskCommentApi => _taskCommentApi.Value;
            public ITaskIdentityLinkApi TaskIdentityLinkApi => _taskIdentityLinkApi.Value;
            public ITaskLocalVariableApi TaskLocalVariableApi => _taskLocalVariableApi.Value;
            public ITaskVariableApi TaskVariableApi => _taskVariableApi.Value;
            public ITelemetryApi TelemetryApi => _telemetryApi.Value;
            public ITenantApi TenantApi => _tenantApi.Value;
            public IUserApi UserApi => _userApi.Value;
            public IVariableInstanceApi VariableInstanceApi => _variableInstanceApi.Value;
            public IVersionApi VersionApi => _versionApi.Value;
        }
    }
}