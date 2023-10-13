const express = require("express");
const router = express.Router();
const {
  addPlayer,
  loginPlayer,
  getPlayer,
  updateScore,
} = require("../controllers/account");
const verifyToken = require("../middleware/veriftjwt");

router.route("/register").post(addPlayer);
router.route("/login").post(loginPlayer);
router.get("/", verifyToken, getPlayer);
router.put("/update", verifyToken, updateScore);

module.exports = router;
