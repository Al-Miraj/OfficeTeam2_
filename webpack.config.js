const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");

module.exports = {
  entry: "./src/index.tsx",
  output: {
    path: path.resolve(__dirname, "dist"), 
    filename: "bundle.js", 
  },
  module: {
    rules: [
      {
        test: /\.(png|jpe?g|gif)$/i,
        type: "asset/resource", 
      },
      {
        test: /\.tsx?$/,
        use: "ts-loader",
        exclude: /node_modules/,
      },
      {
        test: /\.css$/,
        use: ["style-loader", "css-loader"],
      },
    ],
  },  
  plugins: [
    new HtmlWebpackPlugin({
      template: "./src/index.html", 
    }),
  ],
  devServer: {
    static: "./dist", 
    port: 8080,
    open: true, 
  },
  resolve: {
    extensions: [".tsx", ".ts", ".js"], 
  },
};
