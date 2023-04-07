function event_say(choice)
local diagOptions = {}
print(ch)
    --npcDialogue = "Now that I have escaped that dreadful dungeon of the Froglok, I can get back to my alchemical experiments."
    local npcDialogue = ch
SendDialogue(mySession, npcDialogue, diagOptions)
end
