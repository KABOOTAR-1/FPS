const express = require("express");
const app = express();
const body_parser = require("body-parser");
const prouter = require("./routes/account");
const error_handler = require("./middleware/error_handler");

require("dotenv").config();
app.use(express.json());
app.use(body_parser.json());

//database
const db = require("./database/connected");

//routes
app.use("/player", prouter);

//error handler
app.use(error_handler);
//port
const port = process.env.PORT || 4001;

const start = async () => {
  try {
    await db(process.env.MONGO_URI);
    app.listen(port, () => {
      console.log(`This is FPS Server on ${port} `);
    });
  } catch (err) {
    console.log("There is a error");
  }
};

start();
