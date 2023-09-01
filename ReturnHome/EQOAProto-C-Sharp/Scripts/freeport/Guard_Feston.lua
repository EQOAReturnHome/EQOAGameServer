function  event_say(choice)
diagOptions = {}
    npcDialogue = "To the west you will find the Temple of Light, which is a bustling community of Paladins and Clerics. "
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end