const express = require('express');
const db = require('./config/db')
const cors = require('cors')

const app = express();
const PORT = 3002;
app.use(cors());
app.use(express.json())

// Route to get an npc
app.get("/api/getnpc/:name", (req, res) => {

    const name = req.params.name;
    db.query("SELECT * FROM npcs WHERE npc_name = ?", name,
        (err, result) => {
            if (err) {
                console.log(err)
            }
            res.send(result)
        });
});

// Route for creating an npc
app.post('/api/create_npc', (req, res) => {

    const npc_name = req.body.userName;
    const title = req.body.title;
    const text = req.body.text;

    db.query("INSERT INTO npc (title, post_text, user_name) VALUES (?,?,?)", [title, text, username], (err, result) => {
        if (err) {
            console.log(err)
        }
        console.log(result)
    });
})
