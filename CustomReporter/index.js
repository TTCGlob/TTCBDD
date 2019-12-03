Vue.component("feature-row", {
    props: ["feature"],
    template: 
    `<div class="feature-row" v-on:click="featureClick">
        <b>{{feature.name}}</b>
        <span class="feature-details">Duration: {{feature.duration}} Passed: {{feature.scenariosPassed}} Failed: {{feature.scenariosFailed}} </span>
    </div>`,
    methods: {
        featureClick : function() {
            this.$emit("select", this.feature.key)
        }
    }
})

Vue.component("feature-scenarios", {
    props: ["feature"],
    template:
    `<div class="feature-scenarios">
        <scenario-container
            v-for="scenario in feature.scenarios"
            v-bind:key="scenario.key"
            v-bind:scenario="scenario"
            v-bind:class="[{passed: scenario.Pass}, {failed: !scenario.Pass}]"
        ></scenario-container>
    </div>`
})

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
                v-bind:class="[{passed: step.pass}, {failed: !step.pass}]"
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
        <b>{{step.stepType}}</b> {{step.name}}
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
const app = new Vue({
    el: "#app",
    data: {
        data :data,
        features : data.features,
        featureKey: 0
    },
    methods : {
        featureClick : function(key) {
            this.featureKey = key;
        }
    }
})