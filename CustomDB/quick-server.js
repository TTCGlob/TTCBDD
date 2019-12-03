const express = require("express")
const dns = require("dns")
const os = require("os")
const app = express()
app.use(express.json())
var data = {};

app.post("/:endpoint", function(req, res) {
    const {params, body} = req
    const {endpoint} = params
    if (data[endpoint] == undefined) {
        data[endpoint] = []
    }
    const id = data[endpoint].reduce((max, curr) => curr.id > max ? curr.id : max, 0) + 1
    body.id = id
    data[endpoint].push(body)
    console.log(data)
    res.send(body)
})

app.get("/:endpoint", function(req, res) {
    const {endpoint} = req.params
    res.send(data[endpoint] == undefined ? [] : data[endpoint])
})

app.get("/:endpoint/:id", function(req, res) {
    const {endpoint, id} = req.params
    const endpointData = data[endpoint]
    if (endpointData == undefined) {
        res.send([])
        return
    }
    const entry = data[endpoint].filter(e => e.id == id)[0]
    console.log(entry)
    res.send(entry == undefined ? {} : entry)
})

function putOrDelete(doDelete) {
    return function(req, res) {
        const {endpoint, id} = req.params
        const body = req.body
        
    }
}

const port = 5000
dns.lookup(os.hostname(), (error, address, deviceFamily) => {
    if (error) {
        console.log(error)
        return
    }
    const listening = app.listen(Number(port), address, () => {
        console.log(`Server running at ${address} on port ${port}`)
    });
});
