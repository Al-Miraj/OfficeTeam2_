const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");

module.exports = {
  entry: "./src/index.tsx",// Pad naar je entry point
  output: {
    path: path.resolve(__dirname, "dist"), // Output map
    filename: "bundle.js", // Output bestand
  },
  module: {
    rules: [
      {
        test: /\.(png|jpe?g|gif)$/i,
        type: "asset/resource", // Zorg ervoor dat Webpack afbeeldingen bundelt
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
      template: "./src/index.html", // Pad naar je HTML-template
    }),
  ],
  devServer: {
    static: "./dist", // Statische bestanden vanuit de "dist" map
    port: 8080, // Specificeer de poort
    open: true, // Open browser automatisch
  },
  resolve: {
    extensions: [".tsx", ".ts", ".js"], // Automatische extensie-resolutie
  },
};
