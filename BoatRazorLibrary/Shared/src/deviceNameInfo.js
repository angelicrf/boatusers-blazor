const thisPath = require('path')
//const thisprocess = require('process')

const getApplicationName = async () => {
    return await new Promise((resolve, reject) => {
        try {
           
            alert(thisPath.dirname(__filename))
            //alert(thisPath.win32.resolve('deviceNameInfo.js'))
                //'C:\\Users\\\angel\\Desktop\\BoatMauiPlazer\\BoatMauiBlazorApp\\boatusers-blazor\\BoatBlazorServer\\wwwroot\\css\\site.css'))

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
module.exports = { getApplicationName }