import * as jsonServer from "json-server"
import * as dns from "dns"
import * as os from "os"

const server = jsonServer.create()
const router = jsonServer.router('db.json')

const middlewares = jsonServer.defaults()


server.use(middlewares)

server.use((req, res, next) => {
    console.log(req)
    next()
})
const port = 3000
server.use(router)
dns.lookup(os.hostname(), (error, address, deviceFamily) => {
    if (error) {
        console.log(error)
        return
    }
    const listening = server.listen(Number(port), address, () => {
        
        console.log(`Server running at ${address} on port ${port}`)
    });
});