const webpack = require('webpack')
const path = require('path')

module.exports = {
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader"
                }
            }
        ]
    },
    output: {
        path: path.resolve(__dirname, '../wwwroot/'),
        filename: "deviceBundleInfo.js",
        library: "MyLib"
    },
    plugins: [
        new webpack.ProvidePlugin({
            process: 'process/browser'
        }),
        new webpack.DefinePlugin({
            'process.env': JSON.stringify(process.env)
        }),
        new webpack.DefinePlugin({
            'process.env.NODE_ENV': '"production"',
        }),
    ],
    resolve: {
        alias: {
            process: "process/browser"
              // process: path.resolve(__dirname, "./node_modules/process/browser.js")
        },
        fallback: {         
            process: require.resolve('process/browser')
        }
    }
}