const thisPath = require('path')
const thisprocess = require('process')

export const getApplicationName = async () => {
    return await new Promise((resolve, reject) => {
        try {
            alert(`DirName ${thisprocess.cwd()}`)
            const directoryName = thisPath.dirname(__filename)
            alert(`Name is ${directoryName}`)

            //window.webkitRequestFileSystem(PERSISTENT, 0, (fs) => {
            //    alert(`Url is ${fs.root.toURL() }`)
            //}, () => { alert("error") })

            let scripts = document.getElementsByTagName('script')
            let lastScript = scripts[4]
            return resolve(lastScript.src.toString().split('/')[4])
            //}
        } catch (e) {
            console.log(e)
            return reject(e)
        }

    })
}