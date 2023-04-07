-- dockmaster endodl

local coaches = require("Scripts/ports")

 
function  event_say(choice)
diagOptions = {}
    npcDialogue = "I love listening to the waves crash.  I couldn't tell you of a more peaceful existence."
    if (choice:find("Hazinak")) then
        TeleportPlayer(
            mySession,
            GetWorld(coaches.hazinak_ferry.world),
            coaches.hazinak_ferry.x,
            coaches.hazinak_ferry.y,
            coaches.hazinak_ferry.z,
            coaches.hazinak_ferry.facing
        )
    else
        npcDialogue = "Where would you like to go?"
        dialogueOptions = {"I would like passage bound for Hazinak."}
        SendDialogue(mySession, npcDialogue, dialogueOptions)
    end
end
