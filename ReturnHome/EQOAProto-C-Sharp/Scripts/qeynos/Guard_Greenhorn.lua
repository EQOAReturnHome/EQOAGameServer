function  event_say(choice)
diagOptions = {}
    npcDialogue = "The King spares no expense when it comes to the weapons and armor for us Qeynos guards. He means to defend this city well, and it shows."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end