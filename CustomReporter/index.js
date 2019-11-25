Vue.component("scenario-container", {
    props: ["scenario"],
    template:
    `<div class="scenario-container" v-on:click="stepsClick()">
        <h4>{{scenario.name}}</h4>
        <p>{{scenario.description}}</p>
        <p>Tags: <i>{{scenario.Tags.join(", ")}}</i></p>
        <p>Duration: {{scenario.duration}}</p>
        <dl>
            <dt>Steps</dt>
            <dd>Passed: {{scenario.stepsPassed}}</dd>
            <dd>Failed: {{scenario.stepsFailed}}</dd>
        </dl>
        <div class="step-list" v-if="active">
            <step-container
                v-for="step in scenario.steps"
                v-bind:key="step.key"
                v-bind:step="step"
            ></step-container>
        </div>
    </div>`,
    data: function() {return {
        active: false
    }},
    methods: {
        stepsClick: function() {
            this.active = !this.active
        }
    }
})

Vue.component("step-container", {
    props: ["step"],
    template:
    `<div class="step-container" v-on:click.stop="stepClick()">
        <p><b>{{step.stepType}}</b> {{step.name}}</p>
        <ul v-if="active">
            <li>Passed: {{step.pass}}</li>
            <li>Duration: {{step.duration}}</li>
            <li v-if="!step.pass">{{step.StepError}}</li>
        </ul>
    </div>`,
    data: function() { return {
        active: false
    }},
    methods: {
        stepClick: function() {
            this.active = !this.active;
        }
    }
})
const data = {"featuresPassed":0,"featuresFailed":1,"scenariosPassed":5,"scenariosFailed":1,"stepsPassed":21,"stepsFailed":1,"features":[{"name":"Rest Call Class Test","description":null,"duration":"00:00:19.6650933","scenariosPassed":5,"scenariosFailed":1,"stepsPassed":21,"stepsFailed":1,"passed":false,"scenarios":[{"name":"Add a new random employee","description":null,"Tags":["Create","Read"],"duration":"00:00:03.1399414","Pass":true,"stepsPassed":4,"stepsFailed":0,"steps":[{"stepType":"Given","name":"User accesses employees API at \"http://dummy.restapiexample.com/api/v1\"","pass":true,"duration":"00:00:00.0029919","StepError":null},{"stepType":"Given","name":"User creates a new employee","pass":true,"duration":"00:00:00.0009968","StepError":null},{"stepType":"When","name":"User adds the employee to the database","pass":true,"duration":"00:00:01.8085755","StepError":null},{"stepType":"Then","name":"The employee is present in the employees list","pass":true,"duration":"00:00:01.2825252","StepError":null}]},{"name":"Add new employee and then delete them","description":null,"Tags":["Create","Read","Delete"],"duration":"00:00:04.8512785","Pass":true,"stepsPassed":4,"stepsFailed":0,"steps":[{"stepType":"Given","name":"User accesses employees API at \"http://dummy.restapiexample.com/api/v1\"","pass":true,"duration":"00:00:00","StepError":null},{"stepType":"When","name":"User creates new employee with name: \"stovetop\", age: \"69\", and salary \"300\"","pass":true,"duration":"00:00:03.0433655","StepError":null},{"stepType":"Then","name":"The employee is present in the employees list","pass":true,"duration":"00:00:01.1530289","StepError":null},{"stepType":"Then","name":"The new employee is successfully deleted from the database","pass":true,"duration":"00:00:00.6499019","StepError":null}]},{"name":"Add new employee and then delete them","description":null,"Tags":["Create","Read","Delete"],"duration":"00:00:03.4171927","Pass":true,"stepsPassed":4,"stepsFailed":0,"steps":[{"stepType":"Given","name":"User accesses employees API at \"http://dummy.restapiexample.com/api/v1\"","pass":true,"duration":"00:00:00","StepError":null},{"stepType":"When","name":"User creates new employee with name: \"stovetop\", age: \"12\", and salary \"500000\"","pass":true,"duration":"00:00:00.6740411","StepError":null},{"stepType":"Then","name":"The employee is present in the employees list","pass":true,"duration":"00:00:01.2328396","StepError":null},{"stepType":"Then","name":"The new employee is successfully deleted from the database","pass":true,"duration":"00:00:01.5063280","StepError":null}]},{"name":"Get an employee","description":null,"Tags":["Read"],"duration":"00:00:00.6897100","Pass":true,"stepsPassed":3,"stepsFailed":0,"steps":[{"stepType":"Given","name":"User accesses employees API at \"http://dummy.restapiexample.com/api/v1\"","pass":true,"duration":"00:00:00","StepError":null},{"stepType":"When","name":"User accesses employee \"1\"","pass":true,"duration":"00:00:00.6867247","StepError":null},{"stepType":"Then","name":"The employee record is displayed","pass":true,"duration":"00:00:00.0010234","StepError":null}]},{"name":"Updating employee salary","description":null,"Tags":["Read","Update"],"duration":"00:00:06.6086472","Pass":true,"stepsPassed":4,"stepsFailed":0,"steps":[{"stepType":"Given","name":"User accesses employees API at \"http://dummy.restapiexample.com/api/v1\"","pass":true,"duration":"00:00:00","StepError":null},{"stepType":"When","name":"User accesses employee \"1\"","pass":true,"duration":"00:00:02.5272502","StepError":null},{"stepType":"When","name":"User updates employee \"1\" with new salary \"999\"","pass":true,"duration":"00:00:04.0703892","StepError":null},{"stepType":"Then","name":"The new salary \"999\" is reflected in the database","pass":true,"duration":"00:00:00.0070163","StepError":null}]},{"name":"Updating employee salary","description":null,"Tags":["Read","Update"],"duration":"00:00:00.9384396","Pass":false,"stepsPassed":2,"stepsFailed":1,"steps":[{"stepType":"Given","name":"User accesses employees API at \"http://dummy.restapiexample.com/api/v1\"","pass":true,"duration":"00:00:00","StepError":null},{"stepType":"When","name":"User accesses employee \"98370\"","pass":true,"duration":"00:00:00.9115185","StepError":null},{"stepType":"When","name":"User updates employee \"98370\" with new salary \"999\"","pass":false,"duration":"00:00:00.0089272","StepError":{"Source":"TechTalk.SpecFlow","Error":"Object reference not set to an instance of an object.","StackTrace":"   at TTCBDD.StepDefinition.RestCallClassTestSteps.WhenUserUpdatesEmployeeWithNewSalary(String id, String salary) in C:\\Users\\Geordie Winlove\\source\\repos\\TTCGlob\\TTCBDD\\StepDefinition\\RestCallClassTestSteps.cs:line 71\r\n   at lambda_method(Closure , IContextManager , String , String )\r\n   at TechTalk.SpecFlow.Bindings.BindingInvoker.InvokeBinding(IBinding binding, IContextManager contextManager, Object[] arguments, ITestTracer testTracer, TimeSpan& duration) in D:\\a\\1\\s\\TechTalk.SpecFlow\\Bindings\\BindingInvoker.cs:line 69\r\n   at TechTalk.SpecFlow.Infrastructure.TestExecutionEngine.ExecuteStepMatch(BindingMatch match, Object[] arguments) in D:\\a\\1\\s\\TechTalk.SpecFlow\\Infrastructure\\TestExecutionEngine.cs:line 411\r\n   at TechTalk.SpecFlow.Infrastructure.TestExecutionEngine.ExecuteStep(IContextManager contextManager, StepInstance stepInstance) in D:\\a\\1\\s\\TechTalk.SpecFlow\\Infrastructure\\TestExecutionEngine.cs:line 316"}}]}]}]}

const scenarios = data.features
    .map(f => f.scenarios)
    .reduce((scenarios, feature) => scenarios.concat(feature))
scenarios.forEach(console.log)
scenarios.forEach((s, index) => s["key"] = index)
const app = new Vue({
    el: "#app",
    data: {
        scenarios: scenarios
    }
})