const error_handler = (err, req, res, next) => {
  return res.status(500).json({ msg: "Something is wrong" });
};

module.exports = error_handler;
