const express = require("express");
const router = express.Router();
const { addPlayer, loginPlayer, getPlayer } = require("../controllers/account");
const verifyToken = require("../middleware/veriftjwt");

router.route("/register").post(addPlayer);
router.route("/login").post(loginPlayer);
router.get("/", verifyToken, getPlayer);

module.exports = router;
