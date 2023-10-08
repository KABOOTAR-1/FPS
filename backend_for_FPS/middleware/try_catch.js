const try_catch = (fn) => {
  return async (req, res, next) => {
    try {
      await fn(req, res);
    } catch (err) {
      res.send(err.message);
    }
  };
};

module.exports = try_catch;
