local quests = require('Scripts/FreeportQuests')
function  event_say(choice)
    local diagOptions = {}
    local questText = ""
    local npcDialogue = ""
    if (GetPlayerFlags(mySession, "100102") == "1")
    then
        if(choice:find("Good")) then
        diagOptions = {}
        npcDialogue = "Spiritmaster Alshan: It is best that you not wonder off if your guildmaster has put you to a task. Guildmasters expect focus and punctuality."
        elseif(choice:find("Sorry")) then
        diagOptions = {}
        npcDialogue = ""
            multiDialogue = {"Spiritmaster Alshan: You were sent to me for binding, or rather, the binding of your spirit.",
            "Spiritmaster Alshan: When a spirit is bound to a location, that spirit will return to the last location it was bound to if it's body is slain.",
            "Spiritmaster Alshan: The body will rematerialize there along with all of it's equipment. I will bind your spirit to this location now.",
            "Spiritmaster Alshan: Before I send you back to Malsis you must speak with Coachman Ronks. He runs the stables to the south.",
            "Spiritmaster Alshan: You can find him just passed the southern exit of Freeport. Head out the doorway southeast of here, then south along the midroad, then southwest to the stables."
            }
            SendMultiDialogue(mySession, multiDialogue)
            BindPlayer(thisEntity.ObjectID)
            ContinueQuest(mySession, 100102, quests[100102][1].log)
        else
        npcDialogue = "Hello."
        diagOptions = { "Sorry to bother, but Malsis sent me.", "Good day!" }
        end
    elseif (GetPlayerFlags(mySession, "120102") == "1")
    then
        npcDialogue = "Hello."
        diagOptions = { "Sorry to bother, but Azlynn sent me.", "Farewell" }
        if(choice:find("Farewell")) then
        npcDialogue = "Spiritmaster Alshan: It is best that you not wonder off if your guildmaster has put you to a task. Guildmasters expect focus and punctuality."
        elseif(choice:find("Sorry")) then
            multiDialogue = "Spiritmaster Alshan: You were sent to me for binding, or rather, the binding of your spirit.",
            "Spiritmaster Alshan: When a spirit is bound to a location, that spirit will return to the last location it was bound to if it's body is slain.",
            "Spiritmaster Alshan: The body will rematerialize there along with all of it's equipment. I will bind your spirit to this location now.",
            "Spiritmaster Alshan: Before I send you back to Azlynn you must speak with Coachman Ronks. He runs the stables to the south.",
            "Spiritmaster Alshan: You can find him just passed the southern exit of Freeport. Head out the doorway southeast of here, then south along the midroad, then southwest to the stables."
            ContinueQuest(mySession, 120102, quests[120102][1].log)
        end
    elseif (choice:find("bind")) then
        npcDialogue = "Your soul will now return here, playerName."
        BindPlayer(thisEntity.ObjectID)
    elseif (choice:find("Not")) then
        npcDialogue = "Please come back if you change your mind."
    else
        npcDialogue = "Would you like me to bind your spirit to this location, child?"
        diagOptions = {"Yes, please bind my soul.", "Not at this time."}
    end
    SendDialogue(mySession, npcDialogue, diagOptions)
end


