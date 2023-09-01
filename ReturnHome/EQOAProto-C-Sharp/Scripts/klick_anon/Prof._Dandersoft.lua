function  event_say(choice)
diagOptions = {}
    npcDialogue = "My staffs are constructed from only the finest ash trees in Tunaria. These provide high conductivity to the four elements."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end