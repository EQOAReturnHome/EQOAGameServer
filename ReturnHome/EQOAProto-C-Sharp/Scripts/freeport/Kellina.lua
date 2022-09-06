local ch = tostring(choice)
function event_say()
    local diagOptions = {}
    local questText = ""
    local npcDialogue = ""
    if (GetPlayerFlags(mySession, "10012") == "0") then
        if (ch:find("Malsis")) then
            multiDialogue = {
                "Kellina: Oohh, now that isn't that disappointing. Well I suppose I should instruct you. But first you must prove your worth.",
                "Kellina: Equip whatever mighty weapon you may have and slay ants. Bring me 2 cracked ant pincers as proof of your valiant deed.",
                "Kellina: After I receive the pincers I will reward you with a scroll of smoldering aura, a magician's specialty.",
                "Kellina: Now be off. I've wasted enough of my time with you."
            }
            SendMultiDialogue(mySession, multiDialogue)
            SetPlayerFlags(mySession, "10012", "1")
            questText = "Return two cracked ant pincers to Kellina."
            AddQuestLog(mySession, 0, questText)
        else
            diagOptions = {"Actually, Malsis sent me."}
            npcDialogue = "Kellina: I don't have time for chit chat, dear."
            SendDialogue(mySession, npcDialogue, diagOptions)
        end
    elseif (GetPlayerFlags(mySession, "10012") == "1") then
        if (CheckQuestItem(mySession, 4866, 2)) then
            if (ch:find("nevermind")) then
                npcDialogue =
                    "Kellina: It's important that you bring me what I have asked for. What was it now… Ah yes, I need two cracked ant pincers."
            elseif (ch:find("pincers")) then
                multiDialogue = {
                    "Kellina: I suppose I can disregard your folly. You are new after all. And you did complete the task I assigned you.",
                    "Kellina: I am a woman of my word. Take this scroll and study it well. The spell is paltry compared to my power, but it's a start.",
                    "You have finished a quest!",
                    "You have given away a cracked ant pincer.",
                    "You have given away a cracked ant pincer.",
                    "You have received a Smoldering Aura Scroll.",
                    "Kellina: Unfortunately I must assign you another task, but I haven't the energy to deal with a novice right now. Come back later."
                }
                SendMultiDialogue(mySession, multiDialogue)
                GrantXP(mySession, 6900)
                DeleteQuestLog(mySession, 0)
                SetPlayerFlags(mySession, "10012", "99")
                SetPlayerFlags(mySession, "10013", "0")
            else
                npcDialogue = "I don't remember calling for you."
                diagOptions = {"I apologize Lady, but I have the pincers.", "Yes, but...I...nevermind, sorry."}
            end
        else
            npcDialogue =
                "Kellina: It's important that you bring me what I have asked for. What was it now...Ah yes, I need two cracked ant pincers."
        end
    elseif (GetPlayerFlags(mySession, "10013") == "0") then
        if (level >= 4) then
            if (ch:find("continue")) then
                multiDialogue = {
                    "Kellina: Apprenticeship? You must at least wear the blue robe of our caste first if you wish to call yourself my apprentice.",
                    "Kellina: Luckily for you I'm feeling somewhat generous today. I'll enchant the robe for you if you bring me the components."
                }
                SendMultiDialogue(mySession, multiDialogue)
                npcDialogue = "Go on, fetch them quickly"
                diagOptions = {"But...I don't know what they are?"}
                SendDialogue(mySession, npcDialogue, diagOptions)
            elseif(ch:find("know")) then
                multiDialogue = {
                    "Kellina: Soulusek's eye, do I even have to think for you now? Nevermind. What I require is a plain robe, and a silk cord.",
                    "Kellina: Plain robes can be purchased from Merchant Yulia just outside this building.",
                    "Kellina: Merchant Yesam has silk cords for sale just inside the eastern city gate nearest the docks.",
                    "Kellina: I'll also need a ruined bat wing. I'm sure the guards wouldn't mind if you collected the wing from the bat pests outside.",
                    "Kellina: Now shoo. And don't come back until you have everything in proper order.",
                    "You have received a quest!"
                }
                SendMultiDialogue(mySession, multiDialogue)
                questText =
                    "Kellina needs a plain robe from Merchant Yulia, a silk cord from Merchant Yesam, and a ruined bat wing."
                AddQuestLog(mySession, 0, questText)
                SetPlayerFlags(mySession, "10013", "1")
            else
                npcDialogue = "I don't have time for chit chat, dear."
                diagOptions = {"I wish to continue my apprenticeship."}
            end
        else
            npcDialogue =
                "Kellina: Unfortunately I must assign you another task, but I haven't the energy to deal with a novice right now. Come back later."
        end
    elseif (GetPlayerFlags(mySession, "10013") == "1") then
        if
            (CheckQuestItem(mySession, 5002, 1) and CheckQuestItem(mySession, 8314, 1) and
                CheckQuestItem(mySession, 4891, 1))
         then
            if (ch:find("No")) then
                multiDialogue =
                    {"Kellina: You'll need to prove yourself by gathering these items. I need 1 plain robe, 1 silk cord, and 1 ruined bat wing."}
                    SendMultiDialogue(mySession, multiDialogue)
            elseif (ch:find("have")) then
                multiDialogue = {
                    "Kellina: Thank you, the enchantment on this robe shall serve to strengthen your defenses against elemental cold.",
                    "Kellina: You may now be strong enough to help with a task given to me, get some rest first though and come back when you're ready.",
                    "You have finished a quest!",
                    "You have given away plain robe",
                    "You have given away silk cord",
                    "You have given away ruined bat wing",
                    "You have received blue robe",
                    "Kellina: You will find as you use your weapons and armor in battle, they will begin to wear down and lose their effectiveness.",
                    "Kellina: If you visit a blacksmith they will repair your weapons and armor for a price."
                }
                SendMultiDialogue(mySession, multiDialogue)
                GrantXP(mySession, 17000)
                DeleteQuestLog(mySession, 0)
                SetPlayerFlags(mySession, "10013", "99")
                SetPlayerFlags(mySession, "10014", "0")
            else
                npcDialogue = "Have you collected the components?"
                diagOptions = {"I have.", "No."}
            end
        else
            npcDialogue =
                "Kellina: You'll need to prove yourself by gathering these items. I need 1 plain robe, 1 silk cord, and 1 ruined bat wing."
        end
    elseif (GetPlayerFlags(mySession, "10014") == "0") then
        if (level >= 5) then
            if (ch:find("task")) then
                multiDialogue = {
                    "Kellina: I truly hope you are my dear, for the task I have for you is necessarily fraught with peril.",
                    "Kellina: Our Academy depends on caravan shipments from the west for a great deal of our research supplies.",
                    "Kellina: Just recently, a band of highwaymen have been hijacking caravans into Freeport in both the deserts and grasslands.",
                }
                SendMultiDialogue(mySession, multiDialogue)
                npcDialogue = "Will you use the knowledge we have taught you thus far to recover the stolen goods and bring these criminals to justice?"
                diagOptions = {"I will.", "Sorry, I can't help you with this"}
                SendDialogue(mySession, npcDialogue, diagOptions)
            elseif(ch:find("Sorry")) then
                multiDialogue = {"Kellina: Do you fancy yourself your own master? You had best reconsider getting this done or we will have to reconsider your enrollment here."}
                SendMultiDialogue(mySession, multiDialogue)
            elseif(ch:find("will")) then 
                multiDialogue = {
                    "Kellina: Great knowledge will be the reward for your bravery. However this is no challenge to take lightly or alone.",
                    "Kellina: For this you should seek the company of other adventurers in Freeport. The Smiling Serpent tavern is a good place to start.",
                    "Kellina: I am sure that we are not the only guild being made to suffer by these criminals, and that there are others who will help.",
                    "Kellina: Go now my apprentice, and return when you've recovered the stolen goods.",
                    "You have received a quest!"
                }
                SendMultiDialogue(mySession, multiDialogue)
                questText =
                    "Travel west from Freeport to find the highwaymen and retrieve from them the stolen goods. Return to Kellina."
                AddQuestLog(mySession, 0, questText)
                SetPlayerFlags(mySession, "10014", "1")
            else
                npcDialogue = "I don’t have time for chit chat, dear."
                diagOptions = {"I am ready for my next task."}
            end
        else
            npcDialogue =
                "Kellina: Unfortunately I must assign you another task, but I haven't the energy to deal with a novice right now. Come back later."
        end
    elseif (GetPlayerFlags(mySession, "10014") == "1") then
        if
            (CheckQuestItem(mySession, 5002, 1) and CheckQuestItem(mySession, 8314, 1) and
                CheckQuestItem(mySession, 4891, 1))
         then
            if (ch:find("Not")) then
                npcDialogue =
                    "Kellina: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
            elseif (ch:find("have")) then
                multiDialogue = {
                    "Kellina: I see...! To be honest, I wasn't sure if I would ever see you again, but here you are proving yourself more useful…",
                    "Kellina: The Academy, along with the other guilds might owe you a debt. I'm not sure what we would have done without these supplies.",
                    "Kellina: For this, I am proud to have you as a true Magician of The Academy of Arcane Science. I award you with this Motivate Scroll.",
                    "Kellina: I will certainly have more tasks for you, but I need some time to look into a few things. Come see me again later.",
                    "You have given away stolen goods.",
                    "You have finished a quest!",
                    "You have received Motivate.",
                }
                SendMultiDialogue(mySession, multiDialogue)
                GrantXP(mySession, 36500)
                DeleteQuestLog(mySession, 0)
                SetPlayerFlags(mySession, "10014", "99")
                SetPlayerFlags(mySession, "10015", "0")
            else
                npcDialogue = "Have you retrieved the stolen goods?"
                diagOptions = {"I have. Here they are...", "Not quite yet."}
            end
        else
            npcDialogue =
                "Kellina: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
        end
    else
        npcDialogue = "I am quite busy with my students right now. Are you sure you're in the right place?"
    end
    SendDialogue(mySession, npcDialogue, diagOptions)
end
