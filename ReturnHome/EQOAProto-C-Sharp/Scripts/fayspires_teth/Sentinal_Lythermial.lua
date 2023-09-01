function event_say()
diagOptions = {}
    npcDialogue = "We have survived the rise and fall of the Combine Empire, through the wisdom and leadership of Lord Thex. You most certainly owe him your allegiance."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end