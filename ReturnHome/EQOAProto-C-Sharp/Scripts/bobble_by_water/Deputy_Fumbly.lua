function  event_say(choice)
diagOptions = {}
    npcDialogue = "Fendin' off these dark elf attacks has got me thirsty somethin' fierce! I could use a mug o'ale straight away."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end