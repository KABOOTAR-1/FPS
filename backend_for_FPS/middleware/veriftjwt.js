const jwt = require("jsonwebtoken");

const verifyToken = (req, res, next) => {
  try {
    const token = req.header("Authorization");

    if (!token || !token.startsWith("Bearer ")) {
      return res.status(403).send("Access Denied");
    }

    const tokenValue = token.slice(7).trimLeft();

    const verified = jwt.verify(tokenValue, process.env.JWT_SECRET);
    req.user = verified;

    next();
  } catch (err) {
    console.error(err.message);
    if (err.name === "JsonWebTokenError") {
      console.log("Invalid token");
      return res.status(401).json({ error: "Invalid token" });
    }
    return res.status(500).json({ error: "Server error" });
  }
};

module.exports = verifyToken;
