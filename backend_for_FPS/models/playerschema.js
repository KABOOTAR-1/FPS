const mongoose = require("mongoose");

const Pschema = new mongoose.Schema({
  user_name: {
    type: String,
    required: [true, "user_name must be provided"],
    unique: true,
  },
  password: {
    type: String,
    required: [true, "password must be provided"],
    min: 6,
  },
  score: {
    type: Number,
    required: [true],
  },
});

module.exports = mongoose.model("Pschema", Pschema);
