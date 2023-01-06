const path = require("path");
const Webpack = require('webpack');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CleanWebpackPlugin = require('clean-webpack-plugin');
const ExtractSASS = new MiniCssExtractPlugin({ filename: './[name].css' });
const CopyWebpackPlugin = require('copy-webpack-plugin');
const BrowserSyncPlugin = require('browser-sync-webpack-plugin');

module.exports = (options) => {
    let webpackConfig = {
        mode: 'none',
        devtool: options.devtool,
        entry: {
            main: './src/index.js',
            demo: './src/scripts-init/demo.js',
            toastr: './src/scripts-init/toastr.js',
            scrollbar: './src/scripts-init/scrollbar.js',
            fullcalendar: './src/scripts-init/calendar.js',
            maps: './src/scripts-init/maps.js',
            chart_js: './src/scripts-init/charts/chartjs.js',
        },
        output: {
            path: path.resolve(__dirname, '../wwwroot/js'),
            filename: '[name].js'
        },
        plugins: [
            new Webpack.ProvidePlugin({
                $: 'jquery',
                jQuery: 'jquery',
                'window.jQuery': 'jquery',
                Tether: 'tether',
                'window.Tether': 'tether',
                Popper: ['popper.js', 'default'],
            }),
            new CopyWebpackPlugin({
                patterns: [
                    { from: './src/assets/images', to: './assets/images' }
                ]
            }),
            new Webpack.DefinePlugin({
                'process.env': {
                    NODE_ENV: JSON.stringify(options.isProduction ? 'production' : 'development')
                }
            })
        ],
        module: {
            rules: [
                {
                    test: /\.js$/,
                    exclude: /node_modules/,
                    loader: 'babel-loader'
                },
                {
                    test: /\.(woff|woff2|eot|ttf|otf)$/i,
                    type: 'asset/resource',
                },
                {
                    test: /\.(png|svg|jpg|jpeg|gif)$/i,
                    type: 'asset/resource',
                }
            ]
        }
    };

    if (options.isProduction) {
        webpackConfig.entry = [
            './src/index.js',
            './src/scripts-init/demo.js',
            './src/scripts-init/toastr.js',
            './src/scripts-init/scrollbar.js',
            './src/scripts-init/calendar.js',
            './src/scripts-init/maps.js',
            './src/scripts-init/charts/chartjs.js',

        ];

        webpackConfig.plugins.push(
            ExtractSASS,
            new CleanWebpackPlugin([dest], {
                verbose: true,
                dry: false
            })
        );

        webpackConfig.module.rules.push({
            test: /\.scss$/i,
            use: ExtractSASS.extract(['css-loader', 'sass-loader'])
        }, {
            test: /\.css$/i,
            use: ExtractSASS.extract(['css-loader'])
        });

    } else {
        webpackConfig.plugins.push(
            new Webpack.HotModuleReplacementPlugin()
        );

        webpackConfig.module.rules.push({
            test: /\.scss$/i,
            use: ['style-loader', 'css-loader', 'sass-loader']
        }, {
            test: /\.css$/i,
            use: ['style-loader', 'css-loader']
        },
        );

        webpackConfig.devServer = {
            port: options.port,
            historyApiFallback: true,
            hot: !options.isProduction,
        };

        webpackConfig.plugins.push(
            new BrowserSyncPlugin({
                host: 'localhost',
                port: 3002,
                files: ["public/**/*.*"],
                browser: "google chrome",
                reloadDelay: 1000,
            }, {
                reload: false
            })
        );

    }

    return webpackConfig;

};