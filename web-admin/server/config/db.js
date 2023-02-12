const mysql = require('mysql')
const db = mysql.createConnection({
	host: "192.168.6.70",
	user: "fooUser",
	password: "fooPass",
	database:"eqoabase" 
})

module.exports = db;
