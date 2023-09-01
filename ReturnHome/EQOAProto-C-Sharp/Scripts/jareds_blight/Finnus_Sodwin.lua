function  event_say(choice)
diagOptions = {}
    npcDialogue = "A strong gnoll thief named Skarr stole my prized possession, a bronze tablet. I believe he lives in Blackburrow. I am saving up to pay for someone brave to retrieve it. Know anyone by chance?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end