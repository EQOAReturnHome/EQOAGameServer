using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using ReturnHome.Actor;
using ReturnHome.Utilities;

public class WorldServer
{
    private ConcurrentHashSet<Character> _playerList = new();
    private ChannelReader<Character> _chanReader;

    public WorldServer(ChannelReader<Character> chanReader)
    {
        _chanReader = chanReader;
        AddCharacters();
    }

    private async Task AddCharacters()
    {
        //Listen for new incoming connecting characters
        while (await _chanReader.WaitToReadAsync())
            while (_chanReader.TryRead(out Character item))
            {
                _playerList.Add(item);
                Console.WriteLine($"{item.CharName} added to Player List");
            }
    }

    public void CreateObjectUpdates()
    {
        foreach (Character i in _playerList)
        {
            i.DistributeUpdates();
        }
    }
}
