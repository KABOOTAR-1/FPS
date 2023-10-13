const Pschema = require("../models/playerschema");
const try_catch = require("../middleware/try_catch");
const bcrypt = require("bcrypt");
const jwt = require("jsonwebtoken");
const mongoose = require("mongoose");

const removepassword = (user) => {
  const usersWithoutPassword = user.map((u) => {
    const { password, ...userWithoutPassword } = u.toObject();
    return userWithoutPassword;
  });
  return usersWithoutPassword;
};

const addPlayer = try_catch(async (req, res) => {
  const { user_name, password } = req.body;
  const user = Pschema.find({ user_name: user_name });
  if (user.length > 0) return res.status(400).send("201");
  const salt = await bcrypt.genSalt();
  const passwordHash = await bcrypt.hash(password, salt);
  2;
  const newPlayer = new Pschema({
    user_name,
    password: passwordHash,
    score: 0,
  });

  await newPlayer.save();

  const userWithoutPassword = newPlayer.toObject();
  delete userWithoutPassword.password;
  console.log(userWithoutPassword);
  res.send(userWithoutPassword);
});

const loginPlayer = try_catch(async (req, res) => {
  const { user_name, password } = req.body;
  const user = await Pschema.find({ user_name: user_name });
  if (user.length == 0) return res.status(404).send("404");
  const isMatch = await bcrypt.compare(password, user[0].password);
  console.log(isMatch);
  if (!isMatch) return res.status(400).send("400");
  const nopass = removepassword(user);
  const token = jwt.sign({ id: user.user_name }, process.env.JWT_SECRET);
  const mainuser = nopass[0];
  mainuser.token = token;
  console.log(mainuser);
  res.status(200).json(mainuser);
});

const getPlayer = try_catch(async (req, res) => {
  res.send("This works");
});

const updateScore = try_catch(async (req, res) => {
  const { user_name, score } = req.body;
  const player = await Pschema.findOne({ user_name: user_name });
  if (player == null) console.log("No such player exsists");
  console.log("This works");
  if (player.score < score) {
    const find = await GameSC.findOneAndUpdate(
      { user_name: user_name },
      { score: score }
    );
    res.send(find);
  } else {
    res.send(player);
  }
});

module.exports = { addPlayer, loginPlayer, getPlayer, updateScore };
