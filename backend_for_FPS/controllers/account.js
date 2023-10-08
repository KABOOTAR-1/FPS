const Pschema = require("../models/playerschema");
const try_catch = require("../middleware/try_catch");
const bcrypt = require("bcrypt");
const jwt = require("jsonwebtoken");

const removepassword = (user) => {
  const usersWithoutPassword = user.map((u) => {
    const { password, ...userWithoutPassword } = u.toObject();
    return userWithoutPassword;
  });
  return usersWithoutPassword;
};

const addPlayer = try_catch(async (req, res) => {
  const { user_name, password, score } = req.body;
  const user = Pschema.find({ user_name: user_name });
  if (user.length > 0)
    return res.status(400).json({ msg: "Username already exsists" });
  const salt = await bcrypt.genSalt();
  const passwordHash = await bcrypt.hash(password, salt);
  2;
  const newPlayer = await Pschema.create({
    user_name,
    password: passwordHash,
    score,
  });
  const player = removepassword(newPlayer);
  res.send(newPlayer);
});

const loginPlayer = try_catch(async (req, res) => {
  const { user_name, password } = req.body;
  const user = await Pschema.find({ user_name: user_name });
  if (!user) return res.status(400).json({ msg: "User does not exist. " });
  const isMatch = await bcrypt.compare(password, user[0].password);
  console.log(isMatch);
  if (!isMatch) return res.status(400).json({ msg: "Invalid credentials. " });
  const nopass = removepassword(user);
  const token = jwt.sign({ id: user.user_name }, process.env.JWT_SECRET);
  const mainuser = nopass[0];
  mainuser.token = token;
  console.log(mainuser);
  res.status(200).json(mainuser);
});

const getPlayer = try_catch(async (req, res) => {
  console.log(req.user);
  res.send("This works");
});

module.exports = { addPlayer, loginPlayer, getPlayer };
